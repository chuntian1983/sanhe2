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

public partial class SysManage_ReportDesign : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            GridView1.Attributes["onselectstart"] = "return false;";
            SaveSheet.Attributes["onclick"] = "_OnSubmit();";
            InsertRow.Attributes["onclick"] = "return CheckSelRow(0)";
            DeleteRow.Attributes["onclick"] = "return CheckSelRow(1)";
            CellText.Attributes["onblur"] = "wCellText()";
            if (Session["UnitID"].ToString() == "000000")
            {
                UpdateData.Enabled = false;
            }
            else
            {
                UpdateData.Attributes.Add("onclick", "return confirm('您确定需要取自模板库吗？')");
            }
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        curRowID.Value = "";
        curRowIndex.Value = "0";
        ReportTitle.InnerText = ReportList.SelectedItem.Text;
        DataSet ds = MainClass.GetDataSet("select id,ItemName0,RowNo0,ItemExpr0,ItemExpr1,ItemName1,RowNo1,ItemExpr2,ItemExpr3 "
            + "from CW_CollectRowItem where unitid='" + Session["UnitID"] + "' and designid='" + ReportList.SelectedValue + "' order by levelid");
        AllDesignID.Value = ",";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            AllDesignID.Value += ds.Tables[0].Rows[i]["id"].ToString() + ",";
        }
        ds.Tables[0].Columns.Remove("id");
        MainClass.CreateGridView(ReportList.SelectedValue, ds.Tables[0], GridView1);
        int[] CellPos = new int[] { 2, 3, 6, 7 };
        for (int i = 1; i < GridView1.Rows.Count; i++)
        {
            GridView1.Rows[i].Attributes.Add("onclick", "OnRowClick(this.rowIndex,'" + GridView1.Rows[i].ClientID + "')");
            GridView1.Rows[i].Cells[0].Text = GridView1.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
            GridView1.Rows[i].Cells[0].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[0].ClientID + "')");
            GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[1].ClientID + "')");
            if (ReportList.SelectedValue == "000005")
            {
                GridView1.Rows[i].Cells[2].Attributes.Add("ondblclick", "OnCell1Click('" + GridView1.Rows[i].Cells[2].ClientID + "')");
                if (GridView1.Rows[i].Cells[2].Text == "&nbsp;")
                {
                    GridView1.Rows[i].Cells[2].Attributes["ItemExpr"] = "";
                }
                else
                {
                    GridView1.Rows[i].Cells[2].Attributes["ItemExpr"] = GridView1.Rows[i].Cells[2].Text;
                }
                GridView1.Rows[i].Cells[2].Text = "&nbsp;";
                GridView1.Rows[i].Cells[0].Text = GridView1.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
                GridView1.Rows[i].Cells[0].Style["padding-left"] = "15px";
            }
            else
            {
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
                        GridView1.Rows[i].Cells[CellPos[k]].Text = "&nbsp;";
                    }
                }
                GridView1.Rows[i].Cells[4].Text = GridView1.Rows[i].Cells[4].Text.Replace(" ", "&nbsp;&nbsp;");
                GridView1.Rows[i].Cells[5].Text = GridView1.Rows[i].Cells[5].Text.Replace(" ", "&nbsp;&nbsp;");
                GridView1.Rows[i].Cells[4].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[4].ClientID + "')");
                GridView1.Rows[i].Cells[5].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[5].ClientID + "')");
            }
        }
        RowsCount.Value = GridView1.Rows.Count.ToString();
    }
    protected void SaveSheet_Click(object sender, EventArgs e)
    {
        string[] RowItem = Regex.Split(RowItemText.Value, "!_0_!");
        DataSet ds = MainClass.GetDataSet("select * from CW_CollectRowItem where unitid='" + Session["UnitID"] + "' and designid='"
            + ReportList.SelectedValue + "' order by levelid");
        if (ds.Tables[0].Rows.Count == 0)
        {
            int LevelID = 100000;
            for (int i = 0; i < RowItem.Length - 1; i++)
            {
                LevelID++;
                DataRow newRow = ds.Tables[0].NewRow();
                newRow["ID"] = MainClass.GetRecordID("CW_CollectRowItem");
                newRow["UnitID"] = Session["UnitID"];
                newRow["LevelID"] = LevelID.ToString();
                newRow["DesignID"] = ReportList.SelectedValue;
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
        MainClass.UpdateDataSet(ds);
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
        string NewID = MainClass.GetRecordID("CW_CollectRowItem");
        MainClass.ExecuteSQL("insert CW_CollectRowItem(id,unitid,designid)values('" + NewID + "','" + Session["UnitID"] + "','" + ReportList.SelectedValue + "')");
        for (int i = 1; i < IDList.Length - 1; i++)
        {
            LevelID++;
            MainClass.ExecuteSQL("update CW_CollectRowItem set levelid='" + LevelID.ToString() + "' where id='" + IDList[i] + "'");
            if (i.ToString() == curRowIndex.Value)
            {
                LevelID++;
                MainClass.ExecuteSQL("update CW_CollectRowItem set levelid='" + LevelID.ToString() + "' where id='" + NewID + "'");
            }
        }
        InitWebControl();
    }
    protected void DeleteRow_Click(object sender, EventArgs e)
    {
        string[] IDList = AllDesignID.Value.Split(',');
        MainClass.ExecuteSQL("delete from CW_CollectRowItem where id='" + IDList[int.Parse(curRowIndex.Value)] + "'");
        InitWebControl();
    }
    protected void UpdateData_Click(object sender, EventArgs e)
    {
        int LevelID = 100000;
        string ParentID = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + Session["UnitID"].ToString() + "']", "ParentID");
        string Columns = "ID,UnitID,LevelID,DesignID,ItemName0,RowNo0,ItemExpr0,ItemExpr1,ItemName1,RowNo1,ItemExpr2,ItemExpr3";
        DataSet MyData = MainClass.GetDataSet("select " + Columns + " from CW_CollectRowItem where unitid='" + Session["UnitID"].ToString()
            + "' and designid='" + ReportList.SelectedValue + "' order by levelid");
        DataSet ParentData = MainClass.GetDataSet("select " + Columns + " from CW_CollectRowItem where unitid='" + ParentID
            + "' and designid='" + ReportList.SelectedValue + "' order by levelid");
        for (int i = 0; i < MyData.Tables[0].Rows.Count; i++)
        {
            if (i < ParentData.Tables[0].Rows.Count)
            {
                LevelID++;
                MyData.Tables[0].Rows[i]["LevelID"] = LevelID.ToString();
                MyData.Tables[0].Rows[i]["ItemName0"] = ParentData.Tables[0].Rows[i]["ItemName0"];
                MyData.Tables[0].Rows[i]["RowNo0"] = ParentData.Tables[0].Rows[i]["RowNo0"];
                MyData.Tables[0].Rows[i]["ItemExpr0"] = ParentData.Tables[0].Rows[i]["ItemExpr0"];
                MyData.Tables[0].Rows[i]["ItemExpr1"] = ParentData.Tables[0].Rows[i]["ItemExpr1"];
                MyData.Tables[0].Rows[i]["ItemName1"] = ParentData.Tables[0].Rows[i]["ItemName1"];
                MyData.Tables[0].Rows[i]["RowNo1"] = ParentData.Tables[0].Rows[i]["RowNo1"];
                MyData.Tables[0].Rows[i]["ItemExpr2"] = ParentData.Tables[0].Rows[i]["ItemExpr2"];
                MyData.Tables[0].Rows[i]["ItemExpr3"] = ParentData.Tables[0].Rows[i]["ItemExpr3"];
            }
            else
            {
                MyData.Tables[0].Rows[i].Delete();
            }
        }
        for (int i = MyData.Tables[0].Rows.Count; i < ParentData.Tables[0].Rows.Count; i++)
        {
            LevelID++;
            DataRow newRow = MyData.Tables[0].NewRow();
            newRow["ID"] = MainClass.GetRecordID("CW_CollectRowItem");
            newRow["UnitID"] = Session["UnitID"];
            newRow["LevelID"] = LevelID.ToString();
            newRow["DesignID"] = ReportList.SelectedValue;
            newRow["ItemName0"] = ParentData.Tables[0].Rows[i]["ItemName0"];
            newRow["RowNo0"] = ParentData.Tables[0].Rows[i]["RowNo0"];
            newRow["ItemExpr0"] = ParentData.Tables[0].Rows[i]["ItemExpr0"];
            newRow["ItemExpr1"] = ParentData.Tables[0].Rows[i]["ItemExpr1"];
            newRow["ItemName1"] = ParentData.Tables[0].Rows[i]["ItemName1"];
            newRow["RowNo1"] = ParentData.Tables[0].Rows[i]["RowNo1"];
            newRow["ItemExpr2"] = ParentData.Tables[0].Rows[i]["ItemExpr2"];
            newRow["ItemExpr3"] = ParentData.Tables[0].Rows[i]["ItemExpr3"];
            MyData.Tables[0].Rows.Add(newRow);
        }
        MainClass.UpdateDataSet(MyData);
        InitWebControl();
    }
}
