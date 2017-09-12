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

public partial class SysManage_BalanceAlarm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            IndexSubject.Attributes["readonly"] = "readonly";
            IndexSubject.Attributes["onclick"] = "SelSubject(this);";
            UtilsPage.SetTextBoxAutoValue(IndexValue, "0");
            InitWebControl();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_indexmonitor", string.Format("IndexSubject='{0}' and IndexType='{1}' and UnitID='999999'", IndexSubject.Text, IndexType.SelectedValue)))
        {
            PageClass.ShowAlertMsg(this.Page, "该报警参数已存在！");
        }
        else
        {
            MainClass.ExecuteSQL(string.Format("insert into cw_indexmonitor(id,UnitID,IndexSubject,IndexType,IndexValue,IndexState)values('{0}','{1}','{2}','{3}','{4}','1')",
                new string[] { MainClass.GetRecordID("IndexMonitor"), "999999", IndexSubject.Text, IndexType.SelectedValue, IndexValue.Text }));
            PageClass.ShowAlertMsg(this.Page, "报警参数添加成功！");
        }
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        DataSet ds = MainClass.GetDataSet("select id,IndexSubject,IndexType,IndexValue from cw_indexmonitor where UnitID='999999' order by id desc");
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
        TextBox tbox = (TextBox)GridView1.Rows[e.NewEditIndex].Cells[1].Controls[0];
        tbox.Attributes["onclick"] = "SelSubject(this);";
        tbox.Attributes["readonly"] = "readonly";
        TextBox tvalue = (TextBox)GridView1.Rows[e.NewEditIndex].Cells[3].Controls[0];
        UtilsPage.SetTextBoxAutoValue(tvalue, "0");
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        MainClass.ExecuteSQL(string.Format("update cw_indexmonitor set IndexSubject='{0}',IndexType='{1}',IndexValue='{2}' where id='{3}'",
            new string[]{((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text,
                        ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("IndexTypeEdit")).SelectedValue,
                        ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text,
                        GridView1.DataKeys[e.RowIndex].Value.ToString()}));
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (GridView1.EditIndex != e.Row.RowIndex)
            {
                Label lab = (Label)e.Row.Cells[2].FindControl("IndexTypeShow");
                lab.Text = IndexType.Items.FindByValue(lab.Text).Text;
            }
            LinkButton btnMonitor = (LinkButton)e.Row.Cells[4].FindControl("btnMonitor");
            btnMonitor.Attributes["onclick"] = string.Format("return OpenMonitor('{0}','{1}');", GridView1.DataKeys[e.Row.RowIndex].Value.ToString(), btnMonitor.CommandArgument);
            LinkButton btnDelete = (LinkButton)e.Row.Cells[5].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除报警参数吗？')");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        GridView1.EditIndex = -1;
        LinkButton btnDelete = (LinkButton)sender;
        MainClass.ExecuteSQL("delete from cw_indexmonitor where id='" + btnDelete.CommandArgument + "'");
        PageClass.ShowAlertMsg(this.Page, "报警参数删除成功！");
        InitWebControl();
    }
}
