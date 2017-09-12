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

public partial class BillManage_BankStatementList : System.Web.UI.Page
{
    private string[] arrSettleType = { "现金", "现金支票", "转账支票", "电汇凭证", "贷记凭证", "商业承兑汇票", "银行承兑汇票" };

    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            UtilsPage.SetTextBoxCalendar(SDate, "");
            UtilsPage.SetTextBoxCalendar(EDate, "");
            SDate.Text = DateTime.Now.ToString("yyyy-MM-01");
            EDate.Text = DateTime.Now.ToString("yyyy-MM-") + UtilsPage.GetThisMonthLastDay("00");
            SettleSubject.Attributes["onclick"] = "selSubject('SettleSubject','&filter=102')";
            UtilsPage.SetTextBoxReadOnly(SettleSubject);
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = string.Empty;
        if (SettleSubject.Text.Length > 0)
        {
            QueryString += "$SettleSubject='" + SettleSubject.Text + "'";
        }
        if (SDate.Text.Length > 0)
        {
            QueryString += "$SettleDate>='" + SDate.Text + "'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$SettleDate<='" + EDate.Text + "'";
        }
        if (SettleNo.Text.Length > 0)
        {
            QueryString += "$SettleNo like '%" + SettleNo.Text + "%'";
        }
        if (SettleType.SelectedValue != "-")
        {
            QueryString += "$SettleType='" + SettleType.SelectedValue + "'";
        }
        if (CheckState.SelectedValue != "-")
        {
            QueryString += "$CheckState='" + CheckState.SelectedValue + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_billsettle " + QueryString + " order by id asc");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            GridView1.DataSource = ds.Tables[0].DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GridView1.PageIndex + 1) + "/" + GridView1.PageCount + "页";
            DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
            ddl.Items.Clear();
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
            }
            ddl.SelectedIndex = GridView1.PageIndex;
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_billsettle where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = arrSettleType[TypeParse.StrToInt(e.Row.Cells[3].Text, 0)];
            string doMoney = e.Row.Cells[5].Text;
            if (doMoney.StartsWith("-"))
            {
                e.Row.Cells[5].Text = doMoney.Substring(1);
                e.Row.Cells[4].Text = "贷方";
            }
            else
            {
                e.Row.Cells[5].Text = doMoney;
                e.Row.Cells[4].Text = "借方";
            }
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            if (e.Row.Cells[7].Text == "1")
            {
                e.Row.Cells[7].Text = "√";
                btnEdit.Enabled = false;
            }
            else
            {
                e.Row.Cells[7].Text = "○";
            }
            e.Row.Cells[7].Style["font-size"] = "16pt";
            if (btnEdit.Enabled)
            {
                btnEdit.Attributes["onclick"] = string.Concat("return doCheck('BankStatement.aspx?bid=", btnEdit.CommandArgument, "')");
            }
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
            e.Row.Attributes["onmouseover"] = "bgColor=this.style.backgroundColor;this.style.backgroundColor='#dad5cc';fontColor=this.style.color;this.style.color='red';";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor=bgColor;this.style.color=fontColor;";
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
