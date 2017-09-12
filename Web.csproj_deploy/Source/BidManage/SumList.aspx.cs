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
using System.Text;

public partial class BidManage_SumList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            SDate.Attributes["onclick"] = string.Concat("WdatePicker({dateFmt:'yyyy-MM',alwaysUseStartDate:true})");
            EDate.Attributes["onclick"] = string.Concat("WdatePicker({dateFmt:'yyyy-MM',alwaysUseStartDate:true})");
            UtilsPage.SetTextBoxReadOnly(SDate);
            UtilsPage.SetTextBoxReadOnly(EDate);
            UtilsPage.SetTextBoxAutoValue(BidMoney, 0);
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        StringBuilder townid = new StringBuilder();
        DataTable cunits = ValidateClass.GetRegTable("CUnits");
        DataRow[] towns = cunits.Select("ParentID='" + Session["TownID"].ToString() + "'");
        if (towns.Length > 0)
        {
            foreach (DataRow town in towns)
            {
                townid.AppendFormat(",'{0}'", town["ID"].ToString());
            }
            townid.Remove(0, 1);
        }
        else
        {
            townid.AppendFormat("'{0}'", Session["TownID"].ToString());
        }
        string QueryString = "$(TownID in(" + townid.ToString() + "))";
        if (ProFrom.SelectedValue == "0")
        {
            QueryString += "$BookUnit='000000'";
        }
        if (ProFrom.SelectedValue == "1")
        {
            QueryString += "$BookUnit<>'000000'";
        }
        if (ProjectType.SelectedValue != "-")
        {
            QueryString += "$ProjectType='" + ProjectType.SelectedValue + "'";
        }
        if (BidMoney.Text.Length > 0)
        {
            QueryString += "$BidMoney>=" + BidMoney.Text;
        }
        if (SDate.Text.Length > 0)
        {
            QueryString += "$SDate>='" + SDate.Text + "-01'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$EDate<='" + EDate.Text + "-31'";
        }
        if (DueType.SelectedValue != "-")
        {
            if (DueType.SelectedValue == "0")
            {
                QueryString += "$EDate>='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            }
            else
            {
                QueryString += "$EDate<='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataTable data = MainClass.GetDataTable("select id,TownID,ProjectName,ProjectType,BiaoDiMoeny,BidMoney,EDate,(select accountname from cw_account b where b.id=a.accountid) accountname from projects a " + QueryString + " order by id");
        data.Columns.Add("level");
        data.Columns.Add("townname");
        if (data.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, data);
        }
        else
        {
            int pos = 1;
            cunits.PrimaryKey = new DataColumn[] { cunits.Columns["ID"] };
            if (towns.Length > 0)
            {
                foreach (DataRow row in data.Rows)
                {
                    row["level"] = pos.ToString("000");
                    DataRow townrow = cunits.Rows.Find(row["TownID"].ToString());
                    if (townrow != null)
                    {
                        row["townname"] = townrow["UnitName"].ToString();
                    }
                    pos++;
                }
            }
            else
            {
                foreach (DataRow row in data.Rows)
                {
                    row["level"] = pos.ToString("000");
                    row["townname"] = Session["TownName"].ToString();
                    pos++;
                }
            }
            data.Rows.Add(data.NewRow());
            GridView1.DataSource = data.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + data.Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            ddl.Items.Clear();
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            }
            ddl.SelectedIndex = GridView1.PageIndex;
            //汇总
            decimal sum0 = 0;
            decimal sum1 = 0;
            decimal sum2 = 0;
            decimal sum3 = 0;
            foreach (GridViewRow grow in GridView1.Rows)
            {
                sum0 += TypeParse.StrToDecimal(grow.Cells[5].Text, 0);
                sum1 += TypeParse.StrToDecimal(grow.Cells[6].Text, 0);
                sum2 += TypeParse.StrToDecimal(grow.Cells[7].Text, 0);
                sum3 += TypeParse.StrToDecimal(grow.Cells[8].Text, 0);
            }
            GridView1.Rows[GridView1.Rows.Count - 1].Style["color"] = "red";
            GridView1.Rows[GridView1.Rows.Count - 1].Style["background-color"] = "#f1fbf0";
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[0].Text = "合计";
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[3].Text = "";
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[9].Text = "";
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[5].Text = sum0.ToString("#0.00");
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[6].Text = sum1.ToString("#0.00");
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[7].Text = sum2.ToString("#0.00");
            GridView1.Rows[GridView1.Rows.Count - 1].Cells[8].Text = sum3.ToString("#0.00");
        }
    }
    protected void JumpPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridView1.PageIndex = Convert.ToInt32(ddl.SelectedValue);
        InitWebControl();
    }
    protected void FirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        InitWebControl();
    }
    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex -= 1;
            InitWebControl();
        }
    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex += 1;
            InitWebControl();
        }
    }
    protected void LastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
            string pid = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Cells[3].Text = string.Concat("<a href='FileShow.aspx?pid=", pid, "' target=_blank>", e.Row.Cells[3].Text, "</a>");
            if (e.Row.Cells[4].Text == "建设项目")
            {
                decimal sum = TypeParse.StrToDecimal(e.Row.Cells[6].Text, 0) - TypeParse.StrToDecimal(e.Row.Cells[5].Text, 0);
                e.Row.Cells[8].Text = sum.ToString("#0.00");
            }
            else
            {
                decimal sum = TypeParse.StrToDecimal(e.Row.Cells[5].Text, 0) - TypeParse.StrToDecimal(e.Row.Cells[6].Text, 0);
                e.Row.Cells[7].Text = sum.ToString("#0.00");
            }
            if (string.Compare(DateTime.Now.ToString("yyyy-MM-dd"), e.Row.Cells[9].Text) > 0)
            {
                e.Row.Cells[9].Text = "已到期";
            }
            else
            {
                e.Row.Cells[9].Text = "未到期";
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }

    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        PageClass.ToExcel(GridView1, "村民代表会议");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
