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

public partial class BillManage_DetailAccountSum : System.Web.UI.Page
{
    decimal sumqichu = 0;
    decimal sumjie = 0;
    decimal sumdai = 0;
    decimal sumqimo = 0;
    decimal sumqichu9 = 0;
    decimal sumjie9 = 0;
    decimal sumdai9 = 0;
    decimal sumqimo9 = 0;
    string sumsql0 = string.Empty;
    string sumsql1 = string.Empty;
    string sumsql2 = string.Empty;
    string sumsql3 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            UtilsPage.SetTextBoxCalendar(sdate, "yyyy年MM月dd日");
            UtilsPage.SetTextBoxCalendar(edate, "yyyy年MM月dd日");
            GridView1.Attributes.Add("onselectstart", "return false;");
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
        ReportDate.Text = sdate.Text + "～" + edate.Text;
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
        DataTable BindTable = new DataTable();
        for (int i = 0; i < 5; i++)
        {
            BindTable.Columns.Add("F" + i.ToString());
        }
        BindTable.Rows.Add(new string[] { "单位名称", "期初余额", "借方", "贷方", "期末余额" });
        if (CollectUnit.SelectedValue != "000000")
        {
            if (CollectUnit.SelectedValue == "")
            {
                ExeScript.Text = "<script>alert('【" + CollectUnit.SelectedItem.Text + "】无汇总单位！');</script>";
            }
            else
            {
                string sd = string.Empty;
                if (sdate.Text.Length > 0)
                {
                    sd = sdate.Text;
                }
                else
                {
                    sd = "1901年01月01日";
                }
                string ed = string.Empty;
                if (edate.Text.Length > 0)
                {
                    ed = edate.Text;
                }
                else
                {
                    ed = "2099年12月31日";
                }
                string QueryString = string.Empty;
                QueryString += "$VoucherDate>='" + sd + "'";
                QueryString += "$VoucherDate<='" + ed + "'";
                if (QueryString.Length > 0)
                {
                    QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
                }
                //VoucherType='3' VoucherType='2'
                sumsql0 = "select sum(AccMoney) from cw_dayaccount " + QueryString + " and AccMoney>0";
                sumsql1 = "select sum(AccMoney) from cw_dayaccount " + QueryString + " and AccMoney<0";
                sumsql2 = "select sum(AccMoney) from cw_dayaccount where VoucherDate<='" + ed + "'";
                sumsql3 = "select sum(AccMoney) from cw_dayaccount where VoucherDate<'" + sd + "'";
                DataTable gunits = ValidateClass.GetRegTable("CUnits");
                string TotalLevel = ValidateClass.ReadXMLNodeText("FinancialDB/RegInfo", "LastLevel");
                if (CollectUnit.SelectedValue == "XXXXXX")
                {
                    string unitid = Session["UnitID"].ToString();
                    string mylevel = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + unitid + "']", "UnitLevel");
                    if (mylevel == TotalLevel)
                    {
                        DataTable acc = MainClass.GetDataTable("select id,accountname from cw_account where unitid='" + unitid + "'");
                        foreach (DataRow arow in acc.Rows)
                        {
                            sumqichu = 0;
                            sumjie = 0;
                            sumdai = 0;
                            sumqimo = 0;
                            GetCollectData(arow["id"].ToString());
                            BindTable.Rows.Add(new string[] {
                                arow["accountname"].ToString(),
                                sumqichu.ToString("#0.00"),
                                sumjie.ToString("#0.00"),
                                sumdai.ToString("#0.00"),
                                sumqimo.ToString("#0.00") });
                        }
                    }
                    else
                    {
                        DataRow[] urows = gunits.Select("ParentID='" + unitid + "'");
                        foreach (DataRow urow in urows)
                        {
                            sumqichu = 0;
                            sumjie = 0;
                            sumdai = 0;
                            sumqimo = 0;
                            GetCollectUnit(gunits, urow, TotalLevel);
                            BindTable.Rows.Add(new string[] {
                                urow["UnitName"].ToString(),
                                sumqichu.ToString("#0.00"),
                                sumjie.ToString("#0.00"),
                                sumdai.ToString("#0.00"),
                                sumqimo.ToString("#0.00") });
                        }
                    }
                }
                else
                {
                    string[] _G = CollectUnit.SelectedValue.Split('-');
                    string[] accs = _G[0].Split('$');
                    foreach (string uid in accs)
                    {
                        if (uid.Length > 0)
                        {
                            string aname = MainClass.GetFieldFromID(uid, "accountname", "cw_account");
                            if (aname != "NoDataItem")
                            {
                                sumqichu = 0;
                                sumjie = 0;
                                sumdai = 0;
                                sumqimo = 0;
                                GetCollectData(uid);
                                BindTable.Rows.Add(new string[] {
                                aname,
                                sumqichu.ToString("#0.00"),
                                sumjie.ToString("#0.00"),
                                sumdai.ToString("#0.00"),
                                sumqimo.ToString("#0.00") });
                            }
                        }
                    }
                    string[] uuus = _G[1].Split('$');
                    foreach (string uid in uuus)
                    {
                        if (uid.Length > 0)
                        {
                            DataRow[] urows = gunits.Select("ID='" + uid + "'");
                            if (urows.Length > 0)
                            {
                                sumqichu = 0;
                                sumjie = 0;
                                sumdai = 0;
                                sumqimo = 0;
                                GetCollectUnit(gunits, urows[0], TotalLevel);
                                BindTable.Rows.Add(new string[] {
                                urows[0]["UnitName"].ToString(),
                                sumqichu.ToString("#0.00"),
                                sumjie.ToString("#0.00"),
                                sumdai.ToString("#0.00"),
                                sumqimo.ToString("#0.00") });
                            }
                        }
                    }
                }
            }
        }
        //清空余额为零的单元格
        int[] ColumnPos ={ 1, 2, 3, 4 };
        for (int i = 0; i < BindTable.Rows.Count; i++)
        {
            for (int k = 0; k < ColumnPos.Length; k++)
            {
                if (BindTable.Rows[i][ColumnPos[k]].ToString() == "0.00")
                {
                    BindTable.Rows[i][ColumnPos[k]] = "&nbsp;";
                }
            }
        }
        //插入数据
        for (int i = BindTable.Rows.Count - 1; i < 10; i++)
        {
            if (BindTable.Rows.Count > 1)
            {
                BindTable.Rows.InsertAt(BindTable.NewRow(), BindTable.Rows.Count);
            }
            else
            {
                BindTable.Rows.Add(BindTable.NewRow());
            }
        }
        //创建合计行
        DataRow NewRow = BindTable.NewRow();
        NewRow[0] = PageClass.PadRightM("<center>合", 10, "&nbsp;") + "计</center>";
        NewRow[1] = sumqichu9.ToString("#0.00");
        NewRow[2] = sumjie9.ToString("#0.00");
        NewRow[3] = sumdai9.ToString("#0.00");
        NewRow[4] = sumqimo9.ToString("#0.00");
        BindTable.Rows.Add(NewRow);
        //设计表格字段
        GridView1.Columns.Clear();
        BoundField bf = new BoundField();
        bf.ItemStyle.Width = 310;
        bf.DataField = "F0";
        bf.ItemStyle.CssClass = "sbalance";
        bf.HtmlEncode = false;
        GridView1.Columns.Add(bf);
        for (int i = 1; i < 5; i++)
        {
            bf = new BoundField();
            bf.ItemStyle.Width = 110;
            bf.DataField = "F" + i.ToString();
            bf.ItemStyle.CssClass = "sbalance";
            bf.HtmlEncode = false;
            bf.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
            bf.DataFormatString = "{0:f}";
            GridView1.Columns.Add(bf);
        }
        //绑定输出表格
        GridView1.DataSource = BindTable.DefaultView;
        GridView1.DataBind();
        for (int i = 0; i < 5; i++)
        {
            GridView1.Rows[0].Cells[i].Style["color"] = "red";
            GridView1.Rows[0].Cells[i].Style["text-align"] = "center";
        }
    }

    protected void GetCollectUnit(DataTable gunits, DataRow urow, string TotalLevel)
    {
        if (urow["UnitLevel"].ToString() == TotalLevel)
        {
            DataTable acc = MainClass.GetDataTable("select id,accountname from cw_account where unitid='" + urow["ID"].ToString() + "'");
            foreach (DataRow arow in acc.Rows)
            {
                GetCollectData(arow["id"].ToString());
            }
        }
        DataRow[] urows = gunits.Select("parentid='" + urow["ID"].ToString() + "'");
        foreach (DataRow row in urows)
        {
            GetCollectUnit(gunits, row, TotalLevel);
        }
    }

    protected void GetCollectData(string accid)
    {
        UserInfo.AccountID = accid;
        decimal sum = 0;
        decimal sum0 = 0;
        decimal sum1 = 0;
        DataTable sumqichud = CommClass.GetDataTable(sumsql3);
        if (sumqichud.Rows.Count > 0)
        {
            sum = TypeParse.StrToDecimal(sumqichud.Rows[0][0].ToString(), 0);
            sumqichu += sum;
            sumqichu9 += sum;
        }
        DataTable sumjied = CommClass.GetDataTable(sumsql0);
        if (sumjied.Rows.Count > 0)
        {
            sum0 = TypeParse.StrToDecimal(sumjied.Rows[0][0].ToString(), 0);
            sumjie += sum0;
            sumjie9 += sum0;
        }
        DataTable sumdaid = CommClass.GetDataTable(sumsql1);
        if (sumdaid.Rows.Count > 0)
        {
            sum1 = TypeParse.StrToDecimal(sumdaid.Rows[0][0].ToString(), 0);
            sumdai -= sum1;
            sumdai9 -= sum1;
        }
        sumqimo += sum + sum0 + sum1;
        sumqimo9 += sum + sum0 + sum1;
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowIndex < int.Parse(RowsCount.Value))
        {
            //设置单元格编辑属性
            for (int n = 1; n < 8; n++)
            {
                e.Row.Cells[n].Attributes.Add("onclick", "OnCellClick('" + e.Row.Cells[n].ClientID + "')");
            }
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
