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

public partial class FinanceFlow_ReplyList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            UtilsPage.SetTextBoxCalendar(FlowStartDate);
            FlowStartDate.Text = "";
            FlowType.Value = Request.QueryString["flowtype"];
            switch (FlowType.Value)
            {
                case "0":
                    PageTitle.InnerHtml = "资金使用审批";
                    FlowCurrent.Items.Add(new ListItem("资金申请请示", "0"));
                    FlowCurrent.Items.Add(new ListItem("资金使用审批", "1"));
                    FlowCurrent.Items.Add(new ListItem("现金支付单", "2"));
                    break;
                case "1":
                    PageTitle.InnerHtml = "资产租赁审批";
                    FlowCurrent.Items.Add(new ListItem("资产租赁请示", "0"));
                    FlowCurrent.Items.Add(new ListItem("资产租赁评估", "1"));
                    FlowCurrent.Items.Add(new ListItem("资产租赁审批", "2"));
                    FlowCurrent.Items.Add(new ListItem("资产租赁公告", "3"));
                    break;
                case "2":
                    PageTitle.InnerHtml = "资源发包审批";
                    FlowCurrent.Items.Add(new ListItem("资源发包请示", "0"));
                    FlowCurrent.Items.Add(new ListItem("资源发包评估", "1"));
                    FlowCurrent.Items.Add(new ListItem("资源发包审批", "2"));
                    FlowCurrent.Items.Add(new ListItem("资源发包公告", "3"));
                    break;
            }
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        StringBuilder wh = new StringBuilder();
        if (FlowType.Value == "0")
        {
            wh.AppendFormat("$FlowType='{0}'$FlowCurrent<>'0'", FlowType.Value);
        }
        else
        {
            wh.AppendFormat("$FlowType='{0}'$(FlowCurrent='2' or FlowCurrent='3')", FlowType.Value);
        }
        if (FlowName.Text.Length > 0)
        {
            wh.AppendFormat("$FlowName like '%{0}%'", FlowName.Text);
        }
        if (FlowStartDate.Text.Length > 0)
        {
            wh.AppendFormat("$FlowStartDate='{0}'", FlowStartDate.Text);
        }
        if (FlowCurrent.Text != "-")
        {
            wh.AppendFormat("$FlowCurrent='{0}'", FlowCurrent.SelectedValue);
        }
        if (FlowState.Text != "-")
        {
            wh.AppendFormat("$FlowState='{0}'", FlowState.SelectedValue);
        }
        if (wh.Length > 0)
        {
            wh.Remove(0, 1);
            wh.Insert(0, " where ");
            wh.Replace("$", " and ");
        }
        DataTable dt = CommClass.GetDataTable(string.Concat("select * from cw_flowlist ", wh.ToString(), " order by id desc"));
        if (dt.Rows.Count == 0)
        {
            PageClass.BindNoRecords(GridView1, dt);
        }
        else
        {
            GridView1.DataSource = dt.DefaultView;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            Label lb = (Label)GridView1.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + dt.Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        switch (FlowType.Value)
        {
            case "0":
                Response.Redirect("MoneyReply.aspx?id=" + btn.CommandArgument);
                break;
            case "1":
                Response.Redirect("AssetReply.aspx?id=" + btn.CommandArgument);
                break;
            case "2":
                Response.Redirect("ResourceReply.aspx?id=" + btn.CommandArgument);
                break;
        }
    }
    protected void btnVeto_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL(string.Concat("update CW_FlowList set FlowState='3',AuditState='0',ReplyDate='", DateTime.Now.ToString("yyyy-MM-dd"), "' where id='", btn.CommandArgument, "'"));
        CommClass.ExecuteSQL(string.Concat("update cw_flowmoney set ReplyMoney=0 where flowid='", btn.CommandArgument, "'"));
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from CW_FlowList where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnVeto = (LinkButton)e.Row.FindControl("btnVeto");
            if (e.Row.Cells[4].Text == "0" || e.Row.Cells[4].Text == "1")
            {
                btnVeto.Attributes.Add("onclick", "javascript:return confirm('您确定需要否决该流程吗？')");
            }
            else
            {
                btnVeto.Enabled = false;
            }
            e.Row.Cells[2].Text = FlowCurrent.Items.FindByValue(e.Row.Cells[2].Text).Text;
            e.Row.Cells[4].Text = FlowState.Items.FindByValue(e.Row.Cells[4].Text).Text;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
