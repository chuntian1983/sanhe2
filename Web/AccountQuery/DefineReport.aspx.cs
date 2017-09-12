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

public partial class AccountQuery_DefineReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            GridView1.Attributes["onselectstart"] = "return false;";
            DateTime AccountDate = MainClass.GetAccountDate();
            ReportDate.Text = AccountDate.ToString("yyyy年MM月");
            ReportDate.Attributes["readonly"] = "readonly";
            SelYear.Attributes["onchange"] = "OnSelChange(this.value,0);";
            SelMonth.Attributes["onchange"] = "OnSelChange(this.value,1);";
            MainClass.InitAccountYear(SelYear);
            SelYear.Text = AccountDate.Year.ToString();
            SelMonth.Text = AccountDate.Month.ToString("00");
            SaveSheet.Attributes["onclick"] = "_OnSubmit();";
            InsertRow.Attributes["onclick"] = "return CheckSelRow(0)";
            DeleteRow.Attributes["onclick"] = "return CheckSelRow(1)";
            CellText.Attributes["onblur"] = "wCellText()";
            CommClass.InitDropDownList("select id,reportname from cw_definereport", ReportList);
            if (ReportList.Items.Count == 0)
            {
                Response.Redirect("DefineReportList.aspx", true);
            }
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        curRowID.Value = "";
        curRowIndex.Value = "0";
        ReportTitle.InnerText = ReportList.SelectedItem.Text;
        DataSet ds = CommClass.GetDataSet("select id,ItemName0,RowNo0,ItemExpr0,ItemExpr1,ItemName1,RowNo1,ItemExpr2,ItemExpr3 "
            + "from CW_DefineRowItem where defineid='" + ReportList.SelectedValue + "' order by levelid");
        AllDesignID.Value = ",";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            AllDesignID.Value += ds.Tables[0].Rows[i]["id"].ToString() + ",";
        }
        ds.Tables[0].Columns.Remove("id");
        CommClass.CreateGridView("000006", ds.Tables[0], GridView1);
        //报表公式计算
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = "000006";
        clsCalculate.GV = GridView1;
        clsCalculate.ReportDate = ReportDate.Text;
        //数据控件属性设置
        int[] CellPos = new int[] { 2, 3, 6, 7 };
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            GridView1.Rows[i].Attributes.Add("onclick", "OnRowClick(this.rowIndex,'" + GridView1.Rows[i].ClientID + "')");
            GridView1.Rows[i].Cells[0].Text = GridView1.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
            GridView1.Rows[i].Cells[0].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[0].ClientID + "')");
            GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[1].ClientID + "')");
            for (int k = 0; k < CellPos.Length; k++)
            {
                GridView1.Rows[i].Cells[CellPos[k]].Attributes.Add("ondblclick", "OnCell1Click('" + GridView1.Rows[i].Cells[CellPos[k]].ClientID + "')");
                if (GridView1.Rows[i].Cells[CellPos[k]].Text == "&nbsp;")
                {
                    GridView1.Rows[i].Cells[CellPos[k]].Attributes["ItemExpr"] = "";
                }
                else
                {
                    GridView1.Rows[i].Cells[CellPos[k]].Attributes["ItemExpr"] = GridView1.Rows[i].Cells[CellPos[k]].Text;
                    clsCalculate.CalculateExpr(GridView1.Rows[i].Cells[CellPos[k]]);
                }
            }
            GridView1.Rows[i].Cells[4].Text = GridView1.Rows[i].Cells[4].Text.Replace(" ", "&nbsp;&nbsp;");
            GridView1.Rows[i].Cells[5].Text = GridView1.Rows[i].Cells[5].Text.Replace(" ", "&nbsp;&nbsp;");
            GridView1.Rows[i].Cells[4].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[4].ClientID + "')");
            GridView1.Rows[i].Cells[5].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[5].ClientID + "')");
        }
        RowsCount.Value = GridView1.Rows.Count.ToString();
    }
    protected void SaveSheet_Click(object sender, EventArgs e)
    {
        string[] RowItem = Regex.Split(RowItemText.Value, "!_0_!");
        DataSet ds = CommClass.GetDataSet("select * from CW_DefineRowItem where defineid='" + ReportList.SelectedValue + "' order by levelid");
        if (ds.Tables[0].Rows.Count == 0)
        {
            int LevelID = 100000;
            for (int i = 0; i < RowItem.Length - 1; i++)
            {
                LevelID++;
                DataRow newRow = ds.Tables[0].NewRow();
                newRow["ID"] = CommClass.GetRecordID("CW_DefineRowItem");
                newRow["LevelID"] = LevelID.ToString();
                newRow["DefineID"] = ReportList.SelectedValue;
                ds.Tables[0].Rows.Add(newRow);
            }
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (RowItem[i].Length == 0) { continue; }
            string[] Item = Regex.Split(RowItem[i], "!_1_!");
            ds.Tables[0].Rows[i]["ItemName0"] = Item[0];
            ds.Tables[0].Rows[i]["RowNo0"] = Item[1];
            ds.Tables[0].Rows[i]["ItemExpr0"] = Item[2];
            ds.Tables[0].Rows[i]["ItemExpr1"] = Item[3];
            ds.Tables[0].Rows[i]["ItemName1"] = Item[4];
            ds.Tables[0].Rows[i]["RowNo1"] = Item[5];
            ds.Tables[0].Rows[i]["ItemExpr2"] = Item[6];
            ds.Tables[0].Rows[i]["ItemExpr3"] = Item[7];
        }
        CommClass.UpdateDataSet(ds);
        InitWebControl();
        ExeScript.Text = "<script>alert('报表参数设置保存成功')</script>";
    }
    protected void ReportList_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void InsertRow_Click(object sender, EventArgs e)
    {
        int LevelID = 100000;
        string[] IDList = AllDesignID.Value.Split(',');
        string NewID = CommClass.GetRecordID("CW_DefineRowItem");
        CommClass.ExecuteSQL("insert CW_DefineRowItem(id,DefineID)values('" + NewID + "','" + ReportList.SelectedValue + "')");
        for (int i = 1; i < IDList.Length - 1; i++)
        {
            LevelID++;
            CommClass.ExecuteSQL("update CW_DefineRowItem set levelid='" + LevelID.ToString() + "' where id='" + IDList[i] + "'");
            if (i.ToString() == curRowIndex.Value)
            {
                LevelID++;
                CommClass.ExecuteSQL("update CW_DefineRowItem set levelid='" + LevelID.ToString() + "' where id='" + NewID + "'");
            }
        }
        InitWebControl();
    }
    protected void DeleteRow_Click(object sender, EventArgs e)
    {
        string[] IDList = AllDesignID.Value.Split(',');
        CommClass.ExecuteSQL("delete from CW_DefineRowItem where id='" + IDList[int.Parse(curRowIndex.Value)] + "'");
        InitWebControl();
    }
    protected void ClearReportData_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("delete from CW_DefineRowItem where defineid='" + ReportList.SelectedValue + "'");
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
