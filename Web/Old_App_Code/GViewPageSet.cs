using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// GridView表格控件分页设置
/// </summary>
public class GViewPageSet : Page
{
    private GridView GView;
    private string sql;

    public GViewPageSet(string _sql, GridView gView)
	{
        GView = gView;
        sql = _sql;
        DataBound();
	}

    private void DataBound()
    {
        DataSet ds = CommClass.GetDataSet(sql);
        if (ds.Tables[0].Rows.Count == 0)
        {
            PageClass.BindNoRecords(GView, ds);
        }
        else
        {
            GView.DataSource = ds.Tables[0].DefaultView;
            GView.DataKeyNames = new string[] { "id" };
            GView.DataBind();
            Label lb = (Label)GView.BottomPagerRow.Cells[0].FindControl("ShowPageInfo");
            lb.Text = "记录数：" + ds.Tables[0].Rows.Count.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lb.Text += "总页数：" + (GView.PageIndex + 1) + "/" + GView.PageCount + "页";
            if (GView.AllowPaging)
            {
                DropDownList ddl = (DropDownList)GView.BottomPagerRow.Cells[0].FindControl("JumpPage");
                ddl.Items.Clear();
                for (int i = 0; i < GView.PageCount; i++)
                {
                    ddl.Items.Add(new ListItem("第" + (i + 1).ToString() + "页", i.ToString()));
                }
                ddl.SelectedIndex = GView.PageIndex;
                ddl.SelectedIndexChanged += new EventHandler(JumpPage_SelectedIndexChanged);
                LinkButton FirstPage = (LinkButton)GView.BottomPagerRow.Cells[0].FindControl("FirstPage");
                FirstPage.Click += new EventHandler(FirstPage_Click);
                LinkButton PreviousPage = (LinkButton)GView.BottomPagerRow.Cells[0].FindControl("PreviousPage");
                PreviousPage.Click += new EventHandler(PreviousPage_Click);
                LinkButton NextPage = (LinkButton)GView.BottomPagerRow.Cells[0].FindControl("NextPage");
                NextPage.Click += new EventHandler(NextPage_Click);
                LinkButton LastPage = (LinkButton)GView.BottomPagerRow.Cells[0].FindControl("LastPage");
                LastPage.Click += new EventHandler(LastPage_Click);
            }
        }
    }

    private void JumpPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GView.PageIndex = Convert.ToInt32(ddl.SelectedValue);
        DataBound();
    }

    private void FirstPage_Click(object sender, EventArgs e)
    {
        GView.PageIndex = 0;
        DataBound();
    }

    private void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GView.PageIndex > 0)
        {
            GView.PageIndex -= 1;
            DataBound();
        }
    }

    private void NextPage_Click(object sender, EventArgs e)
    {
        if (GView.PageIndex < GView.PageCount)
        {
            GView.PageIndex += 1;
            DataBound();
        }
    }

    private void LastPage_Click(object sender, EventArgs e)
    {
        GView.PageIndex = GView.PageCount;
        DataBound();
    }
}
