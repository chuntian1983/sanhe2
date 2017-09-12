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
using System.Text;

public partial class AccountCollect_CommReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            GridView1.Attributes.Add("onselectstart", "return false;");
            SaveSheet.Attributes["onclick"] = "_OnSubmit();";
            InsertRow.Attributes["onclick"] = "return CheckSelRow(0)";
            DeleteRow.Attributes["onclick"] = "return CheckSelRow(1)";
            CellText.Attributes["onblur"] = "wCellText()";
            //--
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
            DesignID.Value = Request.QueryString["DesignID"];
            switch (DesignID.Value)
            {
                case "000003":
                    ReportTitle.InnerText = "收支明细汇总表";
                    break;
                case "000004":
                    ReportTitle.InnerText = "资产负债汇总表";
                    break;
                case "000005":
                    ReportTitle.InnerText = "收益分配汇总表";
                    break;
                case "000006":
                    ReportTitle.InnerText = "财务公开榜";
                    break;
            }
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
        if (CollectUnit.SelectedValue != "000000")
        {
            if (CollectUnit.SelectedValue == "")
            {
                ExeScript.Text = "<script>alert('【" + CollectUnit.SelectedItem.Text + "】无汇总账套或下级单位！');</script>";
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
                            AllAccount = AllAccount.Replace(AccountList[i] + "$", "");
                        }
                        else
                        {
                            if (string.Compare(StartAccountDate.Substring(0, 8), ReportDate.Text) > 0 || string.Compare(LastCarryDate.Substring(0, 8), ReportDate.Text) < 0)
                            {
                                NoCarryAccount.Append("，" + MainClass.GetFieldFromID(AccountList[i], "accountname", "cw_account"));
                                AllAccount = AllAccount.Replace(AccountList[i] + "$", "");
                            }
                        }
                    }
                    if (NoCarryAccount.Length != 0)
                    {
                        ExeScript.Text = "<script>alert('以下账套尚未提交汇总数据：" + NoCarryAccount.ToString().Substring(1) + "');</script>";
                    }
                    GAccountList.Value = AllAccount.ToString();
                }
            }
        }
        InitGridView();
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

    protected void InitGridView()
    {
        curRowID.Value = "";
        curRowIndex.Value = "0";
        string designID = DesignID.Value;
        ClsCalculate clsCalculate = new ClsCalculate();
        clsCalculate.DesignID = designID;
        clsCalculate.GV = GridView1;
        DataTable rowItems = MainClass.GetDataTable(string.Concat("select id,ItemName0,RowNo0,ItemExpr0,ItemExpr1,ItemName1,RowNo1,ItemExpr2,ItemExpr3 "
            , "from CW_CollectRowItem where unitid='", Session["UnitID"].ToString(), "' and designid='", designID, "' order by levelid"));
        StringBuilder items = new StringBuilder();
        items.Append(",");
        foreach(DataRow row in rowItems.Rows)
        {
            items.AppendFormat("{0},", row["id"].ToString());
        }
        AllDesignID.Value = items.ToString();
        rowItems.Columns.Remove("id");
        MainClass.CreateGridView(designID, rowItems, GridView1);
        if (designID == "000005")
        {
            clsCalculate.AccountList = GAccountList.Value;
            clsCalculate.ReportDate = string.Concat(SelYear.Text, "年12月");
            for (int i = 1; i < GridView1.Rows.Count; i++)
            {
                if (GridView1.Rows[i].Cells[2].Text != "&nbsp;")
                {
                    clsCalculate.CalculateExpr(GridView1.Rows[i].Cells[2]);
                }
                GridView1.Rows[i].Attributes.Add("onclick", "OnRowClick(this.rowIndex,'" + GridView1.Rows[i].ClientID + "')");
                GridView1.Rows[i].Cells[0].Style["padding-left"] = "15px";
                GridView1.Rows[i].Cells[0].Text = GridView1.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
                GridView1.Rows[i].Cells[0].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[0].ClientID + "')");
                GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[1].ClientID + "')");
                GridView1.Rows[i].Cells[2].Attributes.Add("ondblclick", "OnCell1Click('" + GridView1.Rows[i].Cells[2].ClientID + "')");
            }
        }
        else
        {
            clsCalculate.ReportDate = ReportDate.Text;
            int gRowCount = GridView1.Rows.Count;
            int[] CellPos = new int[] { 2, 3, 6, 7 };
            string[] accountList = GAccountList.Value.Split('$');
            for (int m = 0; m < accountList.Length; m++)
            {
                if (accountList[m].Length > 0)
                {
                    UserInfo.AccountID = accountList[m];
                    //计算报表公式
                    for (int i = 1; i < gRowCount; i++)
                    {
                        for (int k = 0; k < CellPos.Length; k++)
                        {
                            clsCalculate.CalculateExpr(GridView1.Rows[i].Cells[CellPos[k]]);
                        }
                    }
                    //还原报表状态
                    for (int i = 1; i < gRowCount; i++)
                    {
                        for (int k = 0; k < CellPos.Length; k++)
                        {
                            TableCell TC = GridView1.Rows[i].Cells[CellPos[k]];
                            if (Regex.IsMatch(TC.Text.Replace(",", ""), "-?\\d+\\.\\d{0,2}"))
                            {
                                object itemValue = TC.Attributes["ItemValue"];
                                if (itemValue == null)
                                {
                                    TC.Attributes["ItemValue"] = TC.Text;
                                }
                                else
                                {
                                    TC.Attributes["ItemValue"] = string.Format("{0}+({1})", itemValue, TC.Text);
                                }
                            }
                            object itemExpr = TC.Attributes["ItemExpr"];
                            if (itemExpr != null)
                            {
                                TC.Text = itemExpr.ToString();
                                TC.Attributes["ItemExpr"] = null;
                            }
                        }
                    }
                    //--
                }
            }
            //合并计算单元格汇总值
            for (int i = 1; i < gRowCount; i++)
            {
                //输出计算值
                for (int k = 0; k < CellPos.Length; k++)
                {
                    TableCell TC = GridView1.Rows[i].Cells[CellPos[k]];
                    if (TC.Text == "&nbsp;")
                    {
                        TC.Attributes["ItemExpr"] = "";
                    }
                    else
                    {
                        TC.Attributes["ItemExpr"] = TC.Text;
                    }
                    TC.Attributes.Add("ondblclick", "OnCell1Click('" + TC.ClientID + "')");
                    //计算汇总值
                    object itemValue = TC.Attributes["ItemValue"];
                    if (itemValue == null)
                    {
                        TC.Text = "&nbsp;";
                    }
                    else
                    {
                        TC.Text = TypeParse.StrToDecimal(ClsCalculate.JSEval(itemValue.ToString().Replace(",", "")).ToString(), 0).ToString("#,##0.00");
                    }
                    TC.Attributes.Remove("ItemValue");
                }
                //格式化项目名称
                GridView1.Rows[i].Cells[0].Text = GridView1.Rows[i].Cells[0].Text.Replace(" ", "&nbsp;&nbsp;");
                GridView1.Rows[i].Cells[4].Text = GridView1.Rows[i].Cells[4].Text.Replace(" ", "&nbsp;&nbsp;");
                //定义公式定义事件
                GridView1.Rows[i].Attributes.Add("onclick", "OnRowClick(this.rowIndex,'" + GridView1.Rows[i].ClientID + "')");
                GridView1.Rows[i].Cells[0].Attributes.Add("onclick", "OnCell0Click('" + GridView1.Rows[i].Cells[0].ClientID + "')");
                GridView1.Rows[i].Cells[1].Attributes.Add("onclick", "OnCell2Click('" + GridView1.Rows[i].Cells[1].ClientID + "')");
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
            + DesignID.Value + "' order by levelid");
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
                newRow["DesignID"] = DesignID.Value;
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

    protected void InsertRow_Click(object sender, EventArgs e)
    {
        int LevelID = 100000;
        string[] IDList = AllDesignID.Value.Split(',');
        string NewID = MainClass.GetRecordID("CW_CollectRowItem");
        MainClass.ExecuteSQL("insert CW_CollectRowItem(id,unitid,designid)values('" + NewID + "','" + Session["UnitID"] + "','" + DesignID.Value + "')");
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
            + "' and designid='" + DesignID.Value + "' order by levelid");
        DataSet ParentData = MainClass.GetDataSet("select " + Columns + " from CW_CollectRowItem where unitid='" + ParentID
            + "' and designid='" + DesignID.Value + "' order by levelid");
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
            newRow["UnitID"] = Session["UnitID"].ToString();
            newRow["LevelID"] = LevelID.ToString();
            newRow["DesignID"] = DesignID.Value;
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
