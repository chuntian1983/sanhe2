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

public partial class SysManage_GatherLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            InitWebControl();
        }
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        string QueryString = "$unitid='" + Session["UnitID"] + "'";
        if (LevelName.Text.Length > 0) { QueryString += "$levelname like '%" + LevelName.Text + "%'"; }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1).Replace("$", " and ");
        }
        DataSet ds = MainClass.GetDataSet("select * from cw_collectlevel " + QueryString + " order by id desc");
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string[] _G = e.Row.Cells[1].Text.Split('-');
            StringBuilder AccountList = new StringBuilder();
            if (_G[0] != "")
            {
                DataRowCollection rows = MainClass.GetDataRows("select accountname from cw_account where id in('" + _G[0].Replace("$", "','") + "')");
                if (rows != null)
                {
                    for (int i = 0; i < rows.Count; i++)
                    {
                        AccountList.Append("，" + rows[i]["accountname"].ToString());
                    }
                }
            }
            if (_G.Length == 2 ? _G[1] != "" : false)
            {
                DataRow[] rows = ValidateClass.GetRegRows("CUnits", "id in('" + _G[1].Replace("$", "','") + "')");
                if (rows != null)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        AccountList.Append("，" + rows[i]["unitname"].ToString());
                    }
                }
            }
            if (AccountList.Length > 0)
            {
                e.Row.Cells[1].Text = AccountList.ToString().Substring(1);
            }
            else
            {
                e.Row.Cells[1].Text = "";
            }
            LinkButton btnDelete = (LinkButton)e.Row.Cells[3].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + e.Row.Cells[0].Text + "”吗？')");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnDelete = (LinkButton)sender;
        MainClass.ExecuteSQL("delete from cw_collectlevel where id='" + btnDelete.CommandArgument + "'");
        InitWebControl();
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Response.Redirect("EditGatherLevel.aspx?id=" + btn.CommandArgument);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
}
