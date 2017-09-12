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

public partial class AccountInit_CTLLogManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        UserInfo.CheckSession();
        if (!IsPostBack)
        {
            Button2.Attributes.Add("onclick", "return confirm('您确定需要执行删除日志操作吗？')");
            QSDate.Attributes.Add("readonly", "readonly");
            QSDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            QSDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].QSDate,'yyyy-mm-dd')");
            QEDate.Attributes.Add("readonly", "readonly");
            QEDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            QEDate.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].QEDate,'yyyy-mm-dd')");
            if (Session["UserType"].ToString() == "0")
            {
                UnitList.Items.Add(new ListItem(UserInfo.AccountName, UserInfo.AccountID));
                RealName.Text = Session["RealName"].ToString();
                UserName.Text = Session["UserName"].ToString();
                RealName.Enabled = false;
                UserName.Enabled = false;
                Button2.Enabled = false;
            }
            else
            {
                if (MainClass.GetSysPara("CTLLogManageRead") != "1")
                {
                    PageClass.UrlRedirect("您没有访问权限！", 3);
                }
                if (MainClass.GetSysPara("CTLLogManageDel") != "1")
                {
                    Button2.Enabled = false;
                }
                DataTable Units = MainClass.GetDataTable("select id,accountname from cw_account where unitid='" + Session["UnitID"].ToString() + "' order by levelid,id");
                foreach (DataRow row in Units.Rows)
                {
                    UnitList.Items.Add(new ListItem(row["accountname"].ToString(), row["id"].ToString()));
                }
            }
            if (UnitList.Items.Count == 0)
            {
                PageClass.UrlRedirect("您下属无账套，暂时不可执行该操作！", 3);
            }
            InitWebControl(false);
        }
    }
    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl(bool DelFlag)
    {
        UserInfo.AccountID = UnitList.SelectedValue;
        string QueryString = "$loguid='" + UnitList.SelectedValue + "'";
        if (Session["UserType"].ToString() == "0")
        {
            QueryString += "$loguser='" + Session["UserName"].ToString() + "'";
        }
        else
        {
            if (RealName.Text.Length > 0) { QueryString += "$logname like '%" + RealName.Text + "%'"; }
            if (UserName.Text.Length > 0) { QueryString += "$loguser like '%" + UserName.Text + "%'"; }
        }
        if (QSDate.Text.Length > 0)
        {
            if (QEDate.Text.Length > 0)
            {
                QueryString += "$logtime between '" + QSDate.Text + " 00:00:00' and '" + QEDate.Text + " 23:59:59'";
            }
            else
            {
                QueryString += "$logtime between '" + QSDate.Text + " 00:00:00' and '" + QSDate.Text + " 23:59:59'";
            }
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        //删除操作日志
        if (DelFlag) { CommClass.ExecuteSQL("delete from cw_logs " + QueryString); }
        //--
        DataSet ds = CommClass.GetDataSet("select id,logcontent,loguser,logname,logtime,loguid,logdefine2 from cw_logs " + QueryString + " order by id desc");
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
        InitWebControl(false);
    }
    protected void FirstPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = 0;
        InitWebControl(false);
    }
    protected void PreviousPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex > 0)
        {
            GridView1.PageIndex -= 1;
            InitWebControl(false);
        }
    }
    protected void NextPage_Click(object sender, EventArgs e)
    {
        if (GridView1.PageIndex < GridView1.PageCount)
        {
            GridView1.PageIndex += 1;
            InitWebControl(false);
        }
    }
    protected void LastPage_Click(object sender, EventArgs e)
    {
        GridView1.PageIndex = GridView1.PageCount;
        InitWebControl(false);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[3].Text = MainClass.GetFieldFromID(e.Row.Cells[3].Text, "accountname", "cw_account");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl(false);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        InitWebControl(true);
    }
}
