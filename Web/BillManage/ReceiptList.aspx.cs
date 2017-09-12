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

public partial class BillManage_ReceiptList : System.Web.UI.Page
{
    private string[] arrReveiveState = { "已开", "已换开发票", "已并开发票" };
    private string[] arrPayType = { "现金", "现金支票", "转账支票", "电汇凭证", "贷记凭证", "商业承兑汇票", "银行承兑汇票" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            UtilsPage.SetTextBoxCalendar(SDate, "");
            UtilsPage.SetTextBoxCalendar(EDate, "");
            SDate.Text = DateTime.Now.ToString("yyyy-MM-01");
            EDate.Text = DateTime.Now.ToString("yyyy-MM-") + UtilsPage.GetThisMonthLastDay("00");
            InitWebControl();
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = string.Empty;
        if (PayUnit.Text.Length > 0)
        {
            QueryString += "$PayUnit like '%" + SDate.Text + "%'";
        }
        if (PayType.SelectedValue != "-")
        {
            QueryString += "$PayType='" + PayType.SelectedValue + "'";
        }
        if (ReveiveState.SelectedValue != "-")
        {
            QueryString += "$ReveiveState='" + ReveiveState.SelectedValue + "'";
        }
        if (SDate.Text.Length > 0)
        {
            QueryString += "$ReceiveDate>='" + SDate.Text + "'";
        }
        if (EDate.Text.Length > 0)
        {
            QueryString += "$ReceiveDate<='" + EDate.Text + "'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_billreceipt " + QueryString + " order by ReceiveNo asc");
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
        CommClass.ExecuteSQL("delete from cw_billreceipt where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Text = arrReveiveState[TypeParse.StrToInt(e.Row.Cells[2].Text, 0)];
            e.Row.Cells[3].Text = arrPayType[TypeParse.StrToInt(e.Row.Cells[3].Text, 0)];
            LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
            if (btnEdit.Enabled)
            {
                btnEdit.Attributes["onclick"] = string.Concat("return doCheck('ReceiptBook.aspx','", btnEdit.CommandArgument, "')");
            }
            LinkButton btnShow = (LinkButton)e.Row.FindControl("btnShow");
            btnShow.Attributes.Add("onclick", string.Concat("return showBill('", btnEdit.CommandArgument, "')"));
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
