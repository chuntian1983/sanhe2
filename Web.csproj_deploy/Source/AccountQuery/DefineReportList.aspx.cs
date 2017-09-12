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

public partial class AccountQuery_DefineReportList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000009")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            InitWebControl();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CommClass.ExecuteSQL("insert into cw_definereport(id,reportname,reportnote)values('"
            + CommClass.GetRecordID("CW_DefineReport") + "','"
            + ReportName.Text + "','"
            + ReportNote.Text + "')");
        ReportName.Text = "";
        ReportNote.Text = "";
        ReportName.Focus();
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        DataSet ds = CommClass.GetDataSet("select id,reportname,reportnote from cw_definereport order by id desc");
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.ExcuteScript(this.Page, "$('Button4').disabled='disabled';");
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
            if (GridView1.AllowPaging)
            {
                DropDownList ddl = (DropDownList)GridView1.BottomPagerRow.Cells[0].FindControl("JumpPage");
                ddl.Items.Clear();
                for (int i = 0; i < GridView1.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
                }
                ddl.SelectedIndex = GridView1.PageIndex;
            }
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

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        InitWebControl();
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        CommClass.ExecuteSQL("update cw_definereport set reportname='"
            + ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text + "',reportnote='"
            + ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtReportNote")).Text
            + "' where id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "'");
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string rname = e.Row.Cells[1].Controls.Count == 0 ? e.Row.Cells[1].Text : ((TextBox)e.Row.Cells[1].Controls[0]).Text;
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除报表“" + rname + "”吗？')");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        LinkButton btnDelete = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from cw_definereport where id='" + btnDelete.CommandArgument + "'");
        CommClass.ExecuteSQL("delete from cw_definerowitem where defineid='" + btnDelete.CommandArgument + "'");
        ExeScript.Text = "<script>alert('报表删除成功！')</script>";
        InitWebControl();
    }
}
