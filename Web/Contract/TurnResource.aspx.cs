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

public partial class Contract_TurnResource : System.Web.UI.Page
{
    private int RunLevel = 0;
    private string QName = "classname";
    private string QTable = "cw_resclass";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!PageClass.CheckVisitQuot("000016")) { return; }
        ExeScript.Text = "";
        if (!IsPostBack)
        {
            AName.Text = UserInfo.AccountName;
            ReportDate.Text = DateTime.Now.ToString("yyyy年MM月dd日");
            SLease.Attributes.Add("readonly", "readonly");
            SLease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].SLease,'yyyy-mm-dd')");
            ELease.Attributes.Add("readonly", "readonly");
            ELease.Attributes.Add("onclick", "popUpCalendar(this,document.forms[0].ELease,'yyyy-mm-dd')");
            InitQList();
            InitWebControl();
        }
    }

    protected void InitQList()
    {
        if (QType.SelectedValue == "1")
        {
            QList.Items.Add(new ListItem(UserInfo.AccountName, "000000"));
            DataSet ds = CommClass.GetDataSet("select id," + QName + " from " + QTable + " order by id");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                QList.Items.Add(new ListItem(ds.Tables[0].Rows[i][QName].ToString(), ds.Tables[0].Rows[i]["id"].ToString()));
            }
        }
        else
        {
            InitSubject("资源", "000");
        }
    }

    protected void InitSubject(string CName, string ParentID)
    {
        QList.Items.Add(new ListItem(new string('.', RunLevel * 6) + CName, ParentID));
        DataSet ds = MainClass.GetDataSet("select id,classname from cw_resclass where parentid='" + ParentID + "' order by id asc");
        RunLevel++;
        if (ParentID == "000") { RunLevel = 1; }
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            InitSubject(row["classname"].ToString(), row["id"].ToString());
        }
        RunLevel--;
    }

    protected void QType_SelectedIndexChanged(object sender, EventArgs e)
    {
        QList.Items.Clear();
        if (QType.SelectedValue == "0")
        {
            QName = "classname";
            QTable = "cw_resclass";
        }
        else
        {
            QName = "deptname";
            QTable = "cw_department";
        }
        InitQList();
        InitWebControl();
    }

    /// <summary>
    /// 数据绑定函数
    /// </summary>
    protected void InitWebControl()
    {
        string QueryString = string.Empty;
        if (CardID.Text.Length > 0) { QueryString += "$cardno='" + CardID.Text + "'"; }
        if (AssetNo.Text.Length > 0) { QueryString += "$resno='" + AssetNo.Text + "'"; }
        if (AssetName.Text.Length > 0) { QueryString += "$resname like '%" + AssetName.Text + "%'"; }
        if (QList.SelectedValue != "000" && QList.SelectedValue != "000000")
        {
            if (QType.SelectedValue == "0")
            {
                QueryString += "$ClassID like '" + QList.SelectedValue + "%'";
            }
            else
            {
                QueryString += "$DeptName like '%" + QList.SelectedValue + "%'";
            }
        }
        if (BookType.SelectedValue != "不限")
        {
            QueryString += "$BookType='" + BookType.SelectedValue + "'";
        }
        string[] Relation = { "=", "<>", ">", "<=", "<", ">=" };
        string R0 = Relation[Relation0.SelectedIndex];
        string R1 = Relation[Relation1.SelectedIndex];
        if (SLease.Text.Length > 0)
        {
            QueryString += "$StartLease " + R0 + " '" + SLease.Text + "'";
        }
        if (ELease.Text.Length > 0)
        {
            QueryString += "$EndLease " + R1 + " '" + ELease.Text + "'";
        }
        if (ContractState.Text.Length > 0)
        {
            QueryString += "$ContractState like '" + ContractState.Text + "%'";
        }
        if (QueryString.Length > 0)
        {
            QueryString = " where " + QueryString.Substring(1, QueryString.Length - 1).Replace("$", " and ");
        }
        DataSet ds = CommClass.GetDataSet("select * from cw_rescard " + QueryString + " order by cardno desc");
        if (ds.Tables[0].Rows.Count == 0)
        {
            OutputDataToExcel.Enabled = false;
            PageClass.BindNoRecords(GridView1, ds);
        }
        else
        {
            OutputDataToExcel.Enabled = true;
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Response.Redirect("ResourceCardShow.aspx?id=" + btn.CommandArgument);
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnShow = (LinkButton)e.Row.FindControl("btnShow");
            btnShow.Attributes["onclick"] = string.Format("return ShowVoucher(0,'{0}');", btnShow.CommandArgument);
            LinkButton btnTurn = (LinkButton)e.Row.FindControl("btnTurn");
            HiddenField hidResAmount = (HiddenField)e.Row.FindControl("ResAmount");
            HiddenField hidHasAmount = (HiddenField)e.Row.FindControl("HasAmount");
            if (hidResAmount.Value == hidHasAmount.Value)
            {
                btnTurn.Enabled = false;
            }
            else
            {
                btnTurn.Attributes["onclick"] = "return BookLeaseCard(this,'" + btnTurn.CommandArgument + "');";
            }
            string strid = btnTurn.CommandArgument;
            string strsql = "select * from cw_ztbxm where xmacount='" + UserInfo.AccountID + "' and xmzyid like '%" + strid + "%' and xmzylb='2'";
            DataTable dt = MainClass.GetDataTable(strsql);
            if (dt.Rows.Count > 0)
            {
                btnTurn.Text = "招标流转";
            }
        }
    }
    protected void QList_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        InitWebControl();
    }
    protected void OutputDataToExcel_Click(object sender, EventArgs e)
    {
        InitWebControl();
        GridView1.Columns[6].Visible = false;
        GridView1.Columns[7].Visible = false;
        PageClass.ToExcel(GridView1);
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
    }
}
