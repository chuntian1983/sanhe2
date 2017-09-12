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

public partial class SysManage_AdminManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            Button1.Attributes.Add("onclick", "return CheckSubmit();");
            UnitList.Items.Add(new ListItem("当前单位", Session["UnitID"].ToString()));
            DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + Session["UnitID"].ToString() + "'");
            if (rows != null)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    UnitList.Items.Add(new ListItem(rows[i]["unitname"].ToString(), rows[i]["id"].ToString()));
                }
            }
            InitWebControl();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (MainClass.CheckExist("cw_users", "username='" + UserName.Text + "'"))
        {
            ExeScript.Text = "<script>alert('此管理员名称【" + UserName.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            MainClass.ExecuteSQL("insert cw_users(id,unitid,realname,username,password,powers,accountid,logincounts)values('"
                + MainClass.GetRecordID("CW_Users") + "','" + UnitList.SelectedValue + "','" + RealName.Text + "','" + UserName.Text + "','"
                + Password.Text + "','000000','000000','0')");
            RealName.Text = "";
            UserName.Text = "";
            UserName.Focus();
        }
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    private void InitWebControl()
    {
        string QueryString = "$username<>'sysadmin'$username<>'" + Session["UserName"] + "'$username<>'FinancailDefaultAdmin'";
        StringBuilder MyUnits = new StringBuilder();
        for (int i = 0; i < UnitList.Items.Count; i++)
        {
            MyUnits.Append(UnitList.Items[i].Value + "','");
        }
        QueryString += "$unitid in('" + MyUnits.ToString() + "')";
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = MainClass.GetDataSet("select * from cw_users" + QueryString + " order by id desc");
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
        ((TextBox)GridView1.Rows[e.NewEditIndex].Cells[0].Controls[0]).Width = (Unit)75;
        ((TextBox)GridView1.Rows[e.NewEditIndex].Cells[1].Controls[0]).Width = (Unit)75;
        ((TextBox)GridView1.Rows[e.NewEditIndex].Cells[2].Controls[0]).Width = (Unit)75;
        DropDownList ddl = (DropDownList)GridView1.Rows[e.NewEditIndex].Cells[3].FindControl("UnitList");
        ddl.Items.Add(new ListItem("当前单位", Session["UnitID"].ToString()));
        Label lb = (Label)GridView1.Rows[e.NewEditIndex].Cells[3].FindControl("Label1");
        DataRow[] rows = ValidateClass.GetRegRows("CUnits", "parentid='" + Session["UnitID"].ToString() + "'");
        if (rows != null)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                ddl.Items.Add(new ListItem(rows[i]["unitname"].ToString(), rows[i]["id"].ToString()));
            }
        }
        ddl.SelectedValue = lb.Text;
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        InitWebControl();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string ID = GridView1.DataKeys[e.RowIndex].Value.ToString();
        TextBox N = (TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0];
        if (MainClass.CheckExist("cw_users", "username='" + N.Text + "' and id<>'" + ID + "'"))
        {
            ExeScript.Text = "<script>alert('此管理员名称【" + N.Text + "】已存在，请更换别的。')</script>";
        }
        else
        {
            string UnitID = ((DropDownList)GridView1.Rows[e.RowIndex].Cells[2].FindControl("UnitList")).SelectedValue;
            MainClass.ExecuteSQL("update cw_users set "
                + "realname='" + ((TextBox)GridView1.Rows[e.RowIndex].Cells[0].Controls[0]).Text.Trim() + "',"
                + "username='" + ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text.Trim() + "',"
                + "password='" + ((TextBox)GridView1.Rows[e.RowIndex].Cells[2].Controls[0]).Text.Trim() + "',"
                + "unitid='" + UnitID + "' "
                + "where id='" + ID + "'");
            GridView1.EditIndex = -1;
        }
        InitWebControl();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = (Label)e.Row.Cells[3].FindControl("Label2");
            if (lb != null)
            {
                e.Row.Cells[3].Text = ValidateClass.ReadXMLNodeText("FinancialDB/CUnits[ID='" + lb.Text + "']", "UnitName");
            }
            LinkButton btnDelete = (LinkButton)e.Row.Cells[4].FindControl("btnDelete");
            btnDelete.Attributes.Add("onclick", "javascript:return confirm('您确定需要删除“" + e.Row.Cells[0].Text + "”吗？')");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btnDelete = (LinkButton)sender;
        MainClass.ExecuteSQL("delete from cw_users where id='" + btnDelete.CommandArgument + "'");
        InitWebControl();
    }
}
