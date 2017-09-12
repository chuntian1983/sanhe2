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

public partial class AccountQuery_IncomeDistribution : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009$100011")) { return; }
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            form1.Attributes["onsubmit"] = "_OnSubmit();";
            int AccountYear = MainClass.GetAccountDate().Year;
            ReportDate.Text = AccountYear.ToString() + "年";
            ReportDate.Attributes["readonly"] = "readonly";
            SelYear.Attributes["onchange"] = "OnSelChange(this.value);";
            MainClass.InitAccountYear(SelYear);
            SelYear.Text = AccountYear.ToString();
            if (Session["Powers"].ToString().IndexOf("000009") == -1)
            {
                SaveSheet.Enabled = false;
                GetTemplate.Enabled = false;
            }
            InitWebControl();
            //写入操作日志
            CommClass.WriteCTL_Log("100016", "报表查询：收益分配表");
            //--
        }
    }
    protected void InitWebControl()
    {
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 3; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        DataRow NewRow;
        DataRowCollection rows = CommClass.GetDataRows("select itemname,rowno,itemexpr0,rowinfo from cw_reportrowitem where designid='000005' order by id");
        if (rows != null)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                NewRow = BindTable.NewRow();
                //行列数据输出
                if (rows[i]["rowinfo"].ToString().IndexOf("!_!SingleRow") != -1)
                {
                    string[] Item = Regex.Split(rows[i]["rowinfo"].ToString(), "!_!");
                    if (Item[1].Length == 0)
                    {
                        //含合计行的单行
                        NewRow[0] = Item[0];
                        NewRow[1] = Item[2];
                        NewRow[2] = Item[3];
                    }
                    else
                    {
                        //纯科目运算公式
                        string[] SList = Item[1].Split(new char[] { '+', '-', '*', '/' });
                        NewRow[0] = Item[0];
                        NewRow[1] = Item[2];
                        NewRow[2] = Item[1];
                        for (int m = 0; m < SList.Length; m++)
                        {
                            string NameNo = CommClass.GetFieldFromNo(SList[m], "subjectname") + "[" + SList[m] + "]";
                            NewRow[2] = NewRow[2].ToString().Replace(SList[m], Item[3].Replace(":", NameNo + ":"));
                        }
                    }
                }
                else if (rows[i]["rowinfo"].ToString().IndexOf("!_!NoExpr") != -1)
                {
                    //固定行数据输出（无科目）
                    if (rows[i]["itemname"].ToString().Length == 0)
                    {
                        NewRow[0] = rows[i]["rowinfo"].ToString().Replace("!_!NoExpr", "");
                    }
                    else
                    {
                        NewRow[0] = rows[i]["itemname"].ToString();
                    }
                    NewRow[1] = "";
                    NewRow[2] = "";
                }
                else
                {
                    NewRow[0] = rows[i]["itemname"].ToString();
                    NewRow[1] = rows[i]["rowno"].ToString();
                    NewRow[2] = rows[i]["itemexpr0"].ToString();
                }
                BindTable.Rows.Add(NewRow);
            }
        }
        BindTable.AcceptChanges();
        CommClass.CreateGridView("000005", BindTable, GridView1);
        RowsCount.Value = GridView1.Rows.Count.ToString();
        //计算报表公式
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = "000005";
        clsCalculate.ReportDate = SelYear.SelectedValue + "年12月";
        clsCalculate.GV = GridView1;
        bool isDefineExpr = Session["Powers"].ToString().IndexOf("000009") != -1;
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            if (isDefineExpr)
            {
                GridView1.Rows[i].Attributes.Add("onclick", "OnRowClick(this.rowIndex,'" + GridView1.Rows[i].ClientID + "')");
                GridView1.Rows[i].Cells[0].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[0].ClientID + "')");
                GridView1.Rows[i].Cells[2].Attributes.Add("ondblclick", "OnCell1Click('" + GridView1.Rows[i].Cells[2].ClientID + "')");
                GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[1].ClientID + "')");
            }
            GridView1.Rows[i].Cells[0].Text = GridView1.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
            GridView1.Rows[i].Cells[0].Style["padding-left"] = "15px";
            clsCalculate.CalculateExpr(GridView1.Rows[i].Cells[2]);
        }
        //横向收益分配表
        if (ReportType.SelectedValue == "1")
        {
            DataTable newDT = new DataTable();
            newDT.Columns.Add("F0");
            newDT.Columns.Add("F1");
            newDT.Columns.Add("F2");
            newDT.Columns.Add("F3");
            newDT.Columns.Add("F4");
            newDT.Columns.Add("F5");
            for (int i = 1; i < 13; i++)
            {
                GridViewRow leftRow = GridView1.Rows[i];
                GridViewRow rightRow = GridView1.Rows[i + 12];
                newDT.Rows.Add(new object[] {
                    leftRow.Cells[0].Text, leftRow.Cells[1].Text, leftRow.Cells[2].Text,
                    rightRow.Cells[0].Text, rightRow.Cells[1].Text, rightRow.Cells[2].Text });
            }
            CommClass.CreateGridView("000011", newDT, GridView1);
            GridView1.RowStyle.Height = 21;
            SaveSheet.Enabled = false;
            RowsCount.Value = "0";
        }
        else
        {
            SaveSheet.Enabled = true;
            if (GridView1.Rows.Count < 35)
            {
                GridView1.RowStyle.Height = 800 / GridView1.Rows.Count;
            }
        }
    }
    protected void GetTemplate_Click(object sender, EventArgs e)
    {
        CommClass.GetTemplate("000005");
        InitWebControl();
    }
    protected void SaveSheet_Click(object sender, EventArgs e)
    {
        string[] RowItem = System.Text.RegularExpressions.Regex.Split(RowItemText.Value, "!_0_!");
        DataSet ds = CommClass.GetDataSet("select * from cw_reportrowitem where designid='000005' order by id");
        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string[] Item = System.Text.RegularExpressions.Regex.Split(RowItem[i], "!_1_!");
                ds.Tables[0].Rows[i]["itemname"] = Item[0].Replace("&nbsp;&nbsp;", " ");
                ds.Tables[0].Rows[i]["rowno"] = Item[1];
                ds.Tables[0].Rows[i]["itemexpr0"] = Item[2];
                ds.Tables[0].Rows[i]["rowinfo"] = ds.Tables[0].Rows[i]["rowinfo"].ToString().Replace("!_!SingleRow", "!_!HasModify");
            }
            CommClass.UpdateDataSet(ds);
        }
        InitWebControl();
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
