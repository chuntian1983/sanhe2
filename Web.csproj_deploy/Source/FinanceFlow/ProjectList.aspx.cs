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

public partial class FinanceFlow_ProjectList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000003")) { return; }
        if (!IsPostBack)
        {
            CommClass.InitSysPara(ProjectType, "100001", "全部");
            InitWebControl();
        }
    }
    protected void InitWebControl()
    {
        StringBuilder wh = new StringBuilder();
        if (ProjectName.Text.Length > 0)
        {
            wh.AppendFormat("$ProjectName like '%{0}%'", ProjectName.Text);
        }
        if (ProjectType.SelectedValue != "000000")
        {
            wh.AppendFormat("$ProjectType='{0}'", ProjectType.SelectedValue);
        }
        if (wh.Length > 0)
        {
            wh.Remove(0, 1);
            wh.Insert(0, " where ");
            wh.Replace("$", " and ");
        }
        DataTable dt = CommClass.GetDataTable(string.Concat("select * from cw_project ", wh.ToString(), " order by id desc"));
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
        Response.Redirect("ProjectInfo.aspx?id=" + btn.CommandArgument);
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_project where id='" + btn.CommandArgument + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ListItem pt = ProjectType.Items.FindByValue(e.Row.Cells[2].Text);
            if (pt == null)
            {
                e.Row.Cells[2].Text = "未知";
            }
            else
            {
                e.Row.Cells[2].Text = pt.Text;
            }
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除吗？')");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
