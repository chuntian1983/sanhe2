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

public partial class SysManage_ReportList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            InitWebControl();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (CommClass.CheckExist("CW_DefineReport", "ReportName='" + ReportName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('此报表名称【" + ReportName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            CommClass.ExecuteSQL("insert CW_DefineReport(id,unitid,ReportName)values('"
                + CommClass.GetRecordID("CW_DefineReport") + "','" + Session["UnitID"] + "','" + ReportName.Text + "')");
            ReportName.Text = "";
            ReportName.Focus();
        }
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        DataSet ds = CommClass.GetDataSet("select * from CW_DefineReport where unitid='" + Session["UnitID"] + "'");
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
        string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        TextBox N = (TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0];
        if (CommClass.CheckExist("CW_DefineReport", "ReportName='" + N.Text + "' and id<>'" + ID + "'"))
        {
            ExeScript.Text = "<script>alert('此报表名称【" + N.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            CommClass.ExecuteSQL("update CW_DefineReport set "
                + "ReportName='" + ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text.Trim() + "' "
                + "where id='" + ID + "'");
            GridView1.EditIndex = -1;
        }
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDelete = (LinkButton)e.Row.Cells[3].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + e.Row.Cells[0].Text + "”吗？')");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnDelete = (LinkButton)sender;
        CommClass.ExecuteSQL("delete from CW_DefineReport where id='" + btnDelete.CommandArgument + "'");
        InitWebControl();
    }
}
